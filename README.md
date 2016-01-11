This application is a demo which shows how to communicate an android phone with windows pc by UDP signals. With this app, you can control any pc softwares/games with your android phone on the fly, just like a ps3 gamepad (you can even simulate the vibration of gamepad). isn't it cool? :-)


( Note: the 31st line of /pcontrol_pc/pcontrol2.0/Form1.cs, which gives the IP adress of pc needs to be replaced by your own ip )

This application consist of a windows exe and an android apk. 

(1) Put windows PC and Android phone in the same network. 

(2) Run the exe on pc, it will show an QRcode, which contains the encrypted IP address of pc, and start a service to receive UDP broadcast.

(3) Scan the QRcode with android apk, which will decrypt the IP address, and ready to send UDP signals to pc. 

(4) Send signals to pc by touching the mobile screen, the signals will be received by the pc service, simulating keyboard stroking or mouse clicking on pc according to the signals
