import matplotlib.pyplot as plt
import numpy as np
import glob
import csv
from decimal import Decimal

import glob
import csv

files = glob.glob("output_*.txt")

ver13s = []
ver10s = []
for file in files:
    with open(file) as f:
        lines = f.readlines()
        ver13s.append(lines[11].split(";")[0])
        ver10s.append(lines[27].split(";")[0])

ver13s = list(map(lambda e: Decimal(e), ver13s))
ver10s = list(map(lambda e: Decimal(e), ver10s))


def arr_len(lower, upper, arr):
    return len(list(filter(lambda e: lower <= e <= upper, arr)))


print(f"Len ver13s: {len(ver13s)}")
print(f"Ver13s in range 5000-7000: {arr_len(5000, 7000, ver13s)}")
print(f"Len ver10s: {len(ver10s)}")
print(f"Ver10s in range 5000-7000: {arr_len(5000, 7000, ver10s)}")

# print(sgx_enabled)
# print()
# print(sgx_disabled)
# exit()
plt.style.use("_mpl-gallery")

# make data
# print(values)


def cm2inch(*tupl):
    inch = 2.54
    if isinstance(tupl[0], tuple):
        return tuple(i / inch for i in tupl[0])
    else:
        return tuple(i / inch for i in tupl)


# plot:
fig, ax = plt.subplots()

bins = range(5000, 7000, 25)
# bins = np.arange(5000, 7000, 25)

n, bins, patches = ax.hist(
    [ver13s, ver10s],
    edgecolor="white",
    bins=bins,
    label=["Ver. 13", "Ver. 10"],
)
plt.legend(loc="upper right")
# plt.xticks(bins)
plt.xlabel("Energy consumed (in uj)")
plt.ylabel("Frequency")
plt.xlim(5000, 7000)
figure = plt.gcf()  # get current figure
figure.set_size_inches(cm2inch(18, 10))
plt.tight_layout()
plt.savefig("newtonsoft.pdf", bbox_inches="tight")
# sax.set(xlim=(0, 10))
# ax.set(ylim=(0, 5_000))

# plt.show()
