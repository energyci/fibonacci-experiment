import matplotlib.pyplot as plt
import numpy as np
import glob
import csv
from decimal import Decimal


sgx_enabled = []
# sgx_enabled = np.loadtxt("10m_enabled.txt", dtype=int, skiprows=1) / 1_000

# with open("10m_enabled.txt") as file:
with open("run2.txt") as file:
    lines = file.readlines()[0:200]
    lines = map(lambda a: int(a) / 1_000, lines)
    sgx_enabled = list(lines)

# print(f"SGX enabled, under or equal 80: {len([x for x in sgx_enabled if x <= 80])}")
# print(f"SGX enabled, over 80: {len([x for x in sgx_enabled if x > 80])}")
print(f"SGX enabled, min: {np.amin(sgx_enabled)}")
print(f"SGX enabled, 95 pct: {np.percentile(sgx_enabled, 95)}")
print(f"SGX enabled, 99 pct: {np.percentile(sgx_enabled, 99)}")
print(f"SGX enabled, max: {np.amax(sgx_enabled)}")
print(f"Len, sgx enabled: {len(sgx_enabled)}")


sgx_disabled = []
# with open("10m_disabled.txt") as file:
with open("run1.txt") as file:
    lines = file.readlines()[0:200]
    lines = map(lambda a: int(a) / 1_000, lines)
    sgx_disabled = list(lines)

# print(f"SGX disabled, under or equal 80: {len([x for x in sgx_disabled if x <= 80])}")
# print(f"SGX disabled, over 80: {len([x for x in sgx_disabled if x > 80])}")
print(f"SGX disabled, min: {np.amin(sgx_disabled)}")
print(f"SGX disabled, 95 pct: {np.percentile(sgx_disabled, 95)}")
print(f"SGX disabled, 99 pct: {np.percentile(sgx_disabled, 99)}")
print(f"SGX disabled, max: {np.amax(sgx_disabled)}")
print(f"Len, sgx disabled: {len(sgx_disabled)}")

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

bins = range(10, 80)

n, bins, patches = ax.hist(
    [sgx_enabled, sgx_disabled],
    edgecolor="white",
    bins=bins,
    label=["SGX enabled", "SGX disabled"],
)
plt.legend(loc="upper right")
# plt.xticks(bins)
plt.xlabel("Delay measured in microseconds, split into bins")
plt.ylabel("Frequency of each bin")
plt.xlim(10, 70)
figure = plt.gcf()  # get current figure
figure.set_size_inches(cm2inch(18, 10))
plt.tight_layout()
plt.savefig("sampling_rate.pdf", bbox_inches="tight")
# sax.set(xlim=(0, 10))
# ax.set(ylim=(0, 5_000))

# plt.show()
