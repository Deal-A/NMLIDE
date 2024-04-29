
import sys
import pandas as pd

a = -7
b = 7
h = 0.5

args = []
x1 = a
x2 = x1

while x1 < b:
    x2 = a
    while x2 < b:
        args.append((round(x1,3),round(x2,3)))
        x2+=h
    x1+=h

arr = {'y':[round(pair[0]**3 - 2*pair[1]**2,3) for pair in args],'x1':[pair[0] for pair in args], 'x2':[pair[1] for pair in args]}



ldf = pd.DataFrame(arr).to_csv('x_3vars.csv',index=True,sep=';')

