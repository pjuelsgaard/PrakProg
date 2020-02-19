main.exe : main.cs complex.dll
	mcs -reference:$(word 2,$^) $<

complex.dll : complex.cs
	mcs -target:library $<
