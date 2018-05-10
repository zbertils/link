@echo off

echo "Building installer using linksetup.iss script"
iscc /O".\" /F"linksetup" ".\linksetup.iss"
