CSFLAGS = -optimize+ -platform:arm
.PHONEY : all
all : out.txt
	cat out.txt

out.txt : helloworld.exe
	mono helloworld.exe > out.txt

helloworld.exe : helloworld.cs
	mcs $(CSFLAGS) -out:helloworld.exe helloworld.cs

#.PHONEY: clean #If there is a risk that there is already a file called clean
clean: 
	rm --force out.txt helloworld.exe
