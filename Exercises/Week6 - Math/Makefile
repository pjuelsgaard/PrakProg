CSFLAGS = -optimize+ -platform:arm
.PHONEY : all
all : out.txt
	cat out.txt

out.txt : main.exe
	mono $< > out.txt

main.exe : main.cs cmath.dll
	mcs $(CSFLAGS) -out:main.exe -reference:cmath.dll -reference:world.dll main.cs
cmath.dll : complex.cs cmath.cs
	mcs $^ -target:library -out:$@
	cp ~/matlib/cmath.dll .


#.PHONEY: clean #If there is a risk that there is already a file called clean
clean: 
	rm -f out.txt main.exe
