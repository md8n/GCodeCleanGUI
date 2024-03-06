using Actions.Clean;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GUI.Models;

namespace GUI.ViewModels;

internal class CleanViewModel : ObservableObject {
    private Clean _clean;
    private string[] _logging;
    private readonly object _loggingLock = new object();

    public string Source {
        get => _clean.Filename;
    }

    public string Logging {
        get => string.Join(Environment.NewLine, _logging);
        set {
            lock (_loggingLock) {
                if (string.IsNullOrWhiteSpace(value)) {
                    _logging = [];
                } else {
                    _logging = _logging.Where(l => l != value).Append(value).ToArray();
                }
            }
        }
    }

    public AsyncRelayCommand SelectCommand { get; private set; }
    public AsyncRelayCommand CleanCommand { get; private set; }

    public CleanViewModel() {
        _clean = new Clean {
            Filename = ""
        };
        _logging = [];
        SelectCommand = new AsyncRelayCommand(Select);
        CleanCommand = new AsyncRelayCommand(Clean, CanClean);
    }

    private async Task Select() {
        var file = await FilePicker.Default.PickAsync(new PickOptions {
            PickerTitle = "Select GCode File"
        });
        if (file == null) {
            RefreshProperties();
            return;
        }
        var fileSource = file.FullPath;
        if (string.IsNullOrWhiteSpace(fileSource)) {
            RefreshProperties();
            return;
        }
        _clean = Models.Clean.Load(fileSource);
        RefreshProperties();
    }

    private bool CanClean() {
        return !string.IsNullOrWhiteSpace(_clean.Filename);
    }

    private void DoLogging(string logMessage) {
        Logging = logMessage;
        OnPropertyChanged(nameof(Logging));
    }

    private async Task Clean() {
        FileInfo fileInfo = new(_clean.Filename);
        var annotate = false;
        var lineNumbers = false;
        var minimise = "";
        decimal tolerance = 0M;
        decimal arcTolerance = 0M;
        decimal zClamp = 0M;

        (tolerance, arcTolerance, zClamp) = CleanOptions.Constrain(tolerance, arcTolerance, zClamp);
        DoLogging($"Clipping and general mathematical tolerance: {tolerance}");
        DoLogging($"Arc simplification tolerance: {tolerance}");
        DoLogging($"Z-axis clamping value (max traveling height): {tolerance}");
        DoLogging("All tolerance and clamping values may be further adjusted to allow for inches vs. millimeters");

        FileInfo tokenDefs = new("tokenDefinitions.json");
        var tokenDefsPath = tokenDefs.GetCleanTokenDefsPath();
        var (tokenDefinitions, _) = tokenDefsPath.LoadAndVerifyTokenDefs();

        if (tokenDefinitions == null) {
            DoLogging("----------");
            DoLogging("The 'tokenDefinitions.json' file was not found, or could not be loaded. Processing will not continue.");
            await Shell.Current.GoToAsync($"..?clean={_clean.Filename}");
            return;
        }

        string lastMessage = "";
        await foreach (string logMessage in CleanAction.ExecuteAsync(fileInfo, annotate, lineNumbers, minimise, tolerance, arcTolerance, zClamp, tokenDefinitions)) {
            DoLogging(logMessage);
            lastMessage = logMessage;
        }
        var result = lastMessage == "Success" ? 0 : 1;
        await Shell.Current.GoToAsync($"..?clean={_clean.Filename}");
    }

    private void RefreshProperties() {
        CleanCommand.NotifyCanExecuteChanged();
        OnPropertyChanged(nameof(Source));
    }
}
