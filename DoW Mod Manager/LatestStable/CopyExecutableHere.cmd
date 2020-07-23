del *.exe
PUSHD ..
copy bin\Release\*.exe LatestStable\
POPD
rename *.exe "DoW Mod Manager.exe"