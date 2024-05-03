import base64
import sys

image_string = base64.b64encode(open(sys.argv[1], "rb").read()).decode()

open("{0}.txt".format(sys.argv[1]), "w").write(image_string)