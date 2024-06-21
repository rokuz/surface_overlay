Surface Overlay
==========
Simple overlay app that makes your touch-only experience on the Surface tablet a bit more pleasant.


![Screenshot 2024-06-20 212649](https://github.com/rokuz/surface_overlay/assets/5437220/79bf27e1-60ae-4c48-bb83-5a4bf432685e)

# License:
All the code is free-to-use under [MIT License](https://github.com/rokuz/surface_overlay/blob/main/LICENSE.md).

# Build
Open solution and build it in Visual Studio.

# Using pre-built version:
If you'd like to use pre-built app instead of building it yourself, please check [Releases](https://github.com/rokuz/surface_overlay/releases)

You can use single executable, however Windows may complain about untrusted publisher on every launch.

To get rid of it, you can use packaged version. You need to trust certificate that you can find in the archieve. During setup of the certificate, you need to select:
1. Store Location -> Local Machine
2. Select the Place all certificates in the following store
3. Click Browse... button
4. Select the **Trusted Root Certification Authorities**

# Credits:

Project is inspired by: https://github.com/Apprehentice/ImgOverlay

Multiple people digged into Windows API:
1. https://stackoverflow.com/questions/12591896/disable-wpf-window-focus
2. https://dzone.com/articles/sending-keys-other-apps
3. https://stackoverflow.com/questions/38774139/show-touch-keyboard-tabtip-exe-in-windows-10-anniversary-edition
4. https://github.com/1j01/node-ahk/blob/e58609f3386c248362593353f7d38e8e65fbb0fa/IronAHK/Rusty/Windows/Input.cs#L8

Keyboard icon is taken from: https://fonts.google.com/icons

Lisense for icons: https://developers.google.com/fonts/docs/material_symbols?hl=en#licensing
