#!/usr/bin/env python3

import os
from pathlib import Path

from cairosvg import svg2png

script_folder = os.path.dirname(os.path.realpath(__file__))

# Convert to png in every subfolder
for file in Path('.').rglob('*.svg'):
    print(f"Converting {file}")
    filename = os.path.join(script_folder, file)
    with open(filename, 'r') as f:
        svg_code = f.read()
        png_filename = filename.replace(".svg", ".png")
        svg2png(bytestring=svg_code,write_to=png_filename,dpi=30000)