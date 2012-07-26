The format of the distortion maps are 1024x768x24bpp (4:3). Source X, Source Y is stored in each RGB pixel with 12 bits each. The distortion routine traverses each pixel in the distortion map - getting SourceX and SourceY, and then gets the pixel to paint from the source. 

Example with Photoshop:
- render clouds into a new image (1024x768), save as distort.psd
- open distort_org.png
- select filter/distort/displace
  - set scales to 100 and press ok
  - select distort.psd (the one you created)
- save distorted image as distort_new.png

- run camtimer /displace (the .cmd file in the folder)
- select distort_new.png and then booboomonkey.jpeg to create a thumb
- copy the output files to filter folder
- done!