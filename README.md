# VU1_Control

Windows tool for using the Streacom VU1 dials as VU meters.

Using the web interface that comes with the meters is too slow to keep up with the music.
So this program talks directly to the meters on the USB port, sending the same commands
as the web server would.

Features:
- Auto detect usb port where meters are connected to.
- User can select an audio input or output device to capture.
- Setup sensitivity and smoothness of the meter movement.
- Setup background color of the leds of the meters.
- Setup which meter to use for left and right channel.
- Auto detect of silence, the leds will turn off after 10 seconds.
  if input is detected again, leds will turn on.
- User settings will be saved in the registry.
- Allow up to 3 input setups, with automatic detection on which to enable.
- Optional automatic sensitivity setting

Please note that you need to stop the vu1 web service before running this program, otherwise it can't open the usb port.
  

![image](https://github.com/user-attachments/assets/58443c15-e5e5-4473-b640-139a981d84cc)
