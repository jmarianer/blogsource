#!/bin/bash
BASE=${1/.cs/}
mcs $BASE.cs Common.cs && mono $BASE.exe
rm $BASE.exe
