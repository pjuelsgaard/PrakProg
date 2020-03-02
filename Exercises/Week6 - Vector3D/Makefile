CSC = mcs

all : out.txt
	cat out.txt

out.txt : main.exe
	mono $< > $@

main.exe : main.cs vector3d.dll
	$(CSC) -out:$@ -reference:$(word 2,$^) $<

vector3d.dll : vector3d.cs
	$(CSC) -target:library -out:$@ $<


.PHONEY:
clean :
	rm -f vector3d.dll




