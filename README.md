# GCodeClean GUI

A graphical user interface for the GCodeClean command line utility. See [GCodeClean](https://github.com/md8n/GCodeClean) for more information about GCodeClean itself.

## Getting Started

Currently there is only an installer of this GUI for Windows (the command line utility supports, Windows, Linux, Raspeberry Pi, and can be easily built for macOS). However, this project includes definitions for macOS, iOS and Android.

## Installing

The installer is 'test' signed. Meaning that you have to accept the installation of the 'test' certificate to complete the installation of the GUI. This is a one-time operation. See below `To install the GUI the first time` for how to do this.

If you try running the installer without doing this first-time install step it will show the following message about the certificate, and will not let you run the installer:
> This app package’s publisher certificate could not be verified. Contact your system administrator or the app developer to obtain a new app package with verified certificates. The root certificate and all immediate certificates of the signature in the app package must be verified (0x800B010A)

Once you have done the first-time install step, at least once, you can use the usual 'msix' installer for any later updates.

### To install the GUI the first time
1. Find the `Install.ps1` file in the same folder as the 'usual' installer
2. Right click on this file and select `Run with Powershell`. Windows will open up terminal window for you, which will show something like:
```
Found package: C:\GitHub\GCodeCleanGUI\GUI\bin\Release\net8.0-windows10.0.19041.0\win10-x64\AppPackages\GUI_1.4.0.0_Test\GUI_1.4.0.0_x64.msix
Found certificate: C:\GitHub\GCodeCleanGUI\GUI\bin\Release\net8.0-windows10.0.19041.0\win10-x64\AppPackages\GUI_1.4.0.0_Test\GUI_1.4.0.0_x64.cer

Before installing this app, you need to do the following:
        - Install the signing certificate
Administrator credentials are required to continue.  Please accept the UAC prompt and provide your administrator password if asked.
Press Enter to continue...:
```
3. Press the `Enter` key to continue... Windows will open the `User Account Control` dialog, and ask you to give administrator approval.
4. Click the `Yes` option to give approval. Windows will now open another terminal window, but this one will have the necessary administrator privileges to continue. It will look something like this:
```
Installing certificate...

You are about to install a digital certificate to your computer's Trusted People certificate store. Doing so carries
serious security risk and should only be done if you trust the originator of this digital certificate.

When you are done using this app, you should manually remove the associated digital certificate. Instructions for doing
 so can be found here: http://go.microsoft.com/fwlink/?LinkId=243053

Are you sure you wish to continue?

[Y] Yes  [N] No  [?] Help (default is "N"):
```
5. Type `y` and press `Enter` to give the necessary approval to install the 'test' certificate. Windows will accept this 'administrator approval', close this dialog and return to the original dialog to complete the installation of the 'test' certificate, and the GCodeClean GUI application.
6. Press `Enter` to continue... Windows will close the terminal.

Click on the Windows start icon, search for 'GCodeClean GUI' and click on it - just like any other Windows GUI app.

## Updating / Uninstalling

Currently the GCodeClean GUI installer does NOT support in-place updates. To update it, you first need to uninstall it.
1. Click on the Windows start icon, search for `add or remove programs'.
2. Select `Add or Remove Programs`. Windows will show the list of installed apps.
3. Scroll down the list until you find `GCodeClean GUI` and uninstall it in exactly the same way as any other Windows app.

Note that this will not uninstall the 'test' certificate.

### To uninstall the 'test' certificate
Note that if you uninstall the 'test' certificate and later want to reinstall the GCodeClean GUI you will need to follow the 'first time' installation procedure again.
1. Click on the Windows `start` button, and in the search box type `certmgr`. It should show the option `Manage Computer certificates (Control panel)`.
2. Click on this option. Windows will show the `User Account Control` dialog and ask you to give administrator approval. Do that...
3. Give the approval to Windows. It will now show the 'Control Panel' for the certificate information. WARNING: BE EXTREMELY CAREFUL WHAT YOU DO HERE, YOU CAN REALLY MESS THINGS UP!
4. In the tab bar click on `Action` and select `Find Certificates...`. Windows will show a `Find Certificates` dialog.
5. In the `Contains:` field enter 'md8n' (with the quotes) and click `Find Now`. The 'test' certificate details will show up, assuming it was installed correctly, if nothing shows up continue with the last two steps below.
6. Click once on the certificate details to highlight it, right-mouse click to bring up the context menu, and click on `Delete`. Windows will open a small `Certificates` dialog that asks you "Permanently delete the selected certificate?".
7. Click `Yes`. The 'test' certificate will be removed from the list.
8. Close the `Find Certificates` dialog, `File->Close` or click the `X` in the top right hand corner.
9. Close the 'Control Panel', `File->Exit` or click the `X` in the top right hand corner. 