# VU1_Control

Windows tool for using the Streacom VU1 dials as VU meters.

Using the web interface that comes with the meters is too slow to keep up with the music.
So this program talks directly to the meters on the USB port, sending the same commands
as the web server would.

Features:
- Auto detect usb port where meters are connected to.
  (Caveat: If there's another usb device on the pc using a bridge chip from Future Technology as UART,
   detection could go wrong. This is the same with Streacoms web server...)
- User can select an audio input or output device to capture.
- Setup sensitivity and smoothness of the meter movement.
- Setup background color of the leds of the meters.
- Setup which meter to use for left or right channel.
- Auto detect of silence, the leds will turn off after 10 seconds.
  if input is detected again, leds will turn on.
- User settings will be saved in the registry.