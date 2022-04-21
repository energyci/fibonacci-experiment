import matplotlib.pyplot as plt
import numpy as np
import glob
import csv
from decimal import Decimal

plt.style.use("_mpl-gallery")

# make data
files = glob.glob("output_*_1.csv")

values = []

for file in files:
    with open(file) as csvfile:
        reader = csv.DictReader(csvfile, delimiter=";")
        for row in reader:
            # print(row)
            # exit()
            val = int(row["RAPL-Value"])
            val2 = Decimal(row["Ticks"])
            values.append(val / val2)
            # if val > 0:
            # values.append(val)
            # val = int(row['Ticks'])/1_000
            # if val < 1_000:
            #  values.append(val)
            # values.append(int(row['Ticks'])/1_000)

print(values)

# np.random.seed(1)
# x = 4 + np.random.normal(0, 1.5, 200)

# print(values)
bins = range(10, 80, 5)

# plot:
fig, ax = plt.subplots()

ax.hist(values, bins=bins, edgecolor="white")

# sax.set(xlim=(0, 10))
# ax.set(ylim=(0, 5_000))

plt.show()
