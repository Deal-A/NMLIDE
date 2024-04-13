import base64
import sys

image_string = base64.b64encode(open(sys.argv[1], "rb").read()).decode()

open("image_base64.txt", "w").write(image_string)