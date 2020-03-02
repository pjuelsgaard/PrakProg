all: out.txt
	cat $<

out.txt : main.exe
	mono $< > $@


main.exe:main.cs matlib.dll
	mcs $< -reference:$(word 2, $^)

LIBDIR = ~/matlib
SRC = $(LIBDIR)/integration/quado8a.cs
SRC += $(LIBDIR)/integration/quado4a.cs

matlib.dll : $(SRC)
	mcs $^ -target:library -out:$@



.PHONEY:
clean :
	rm -f out.* main.exe matlib.dll
