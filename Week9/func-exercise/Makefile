all: out.txt
	cat $<

C.svg : C.gpi plotData.txt
	gnuplot $<


out.txt plotData.txt: AB.exe C.exe
	mono $< > out.txt
	mono $(word 2, $^) 1>> out.txt 2>plotData.txt

AB.exe: AB.cs matlib.dll
	mcs $< -reference:$(word 2, $^)

C.exe : C.cs matlib.dll
	mcs $< -reference:$(word 2, $^)

LIBDIR = ~/matlib
SRC = $(LIBDIR)/integration/quado8a.cs
SRC += $(LIBDIR)/integration/quado4a.cs

matlib.dll : $(SRC)
	mcs $^ -target:library -out:$@



.PHONEY:
clean :
	rm -f out* *.exe *.dll plotData.txt
