CSC = mcs
main.exe: main.cs vector.dll
	$(CSC) -reference:$(word 2,$^) main.cs
vector.dll : vector.cs
	$(CSC) -target:library -out:$@ $<
