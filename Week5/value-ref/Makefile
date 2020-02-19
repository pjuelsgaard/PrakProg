CSFLAGS = -optimize+ -platform:arm
.PHONEY : all
all : out.txt
	cat out.txt

out.txt : main.exe
	mono main.exe > out.txt

main.exe : main.cs cmath.dll
	mcs $(CSFLAGS) -out:main.exe -reference:cmath.dll main.cs
cmath.dll : cmath.cs
	mcs $(CSFLAGS) -out:cmath.dll -target:library cmath.cs

#.PHONEY: clean #If there is a risk that there is already a file called clean
clean: 
	rm --force out.txt main.exe
