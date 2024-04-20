
import sys
import pandas as pd

b = -35
f = 35
arr = {'y':[i**3 for i in range(b,f)],'x':[i for i in range(b,f)]}



ldf = pd.DataFrame(arr).to_csv('x_3.csv',index=False,sep=';')

# open("{0}.csv".format(sys.argv[1]), "w").write(image_string)