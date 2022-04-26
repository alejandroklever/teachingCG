import struct
import sys

import matplotlib.pyplot as plt
import numpy as np


def draw_image(file_name):
    with open(file_name, 'rb') as file:
        width, height = struct.unpack('ii', file.read(4 * 2))
        image_data_bytes = file.read((width * height * 4) * 4)
        image_data_float = struct.unpack('f' * (width * height * 4), image_data_bytes)
        np_image = np.array(image_data_float).reshape((height, width, 4))[:, :, 0:3]
        plt.imshow(np_image)
        plt.show()


if __name__ == "__main__":
    fileName = sys.argv[1]
    draw_image(fileName)
