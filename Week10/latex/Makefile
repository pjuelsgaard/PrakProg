all: ms.pdf

main.exe: main.cs exp.cs matlib.dll 
	mcs $< $(word 2,$^) -reference:matlib.dll

ms.pdf : ms.tex plotexp.tex
	pdflatex $<
	pdflatex $<
	bibtex

plotexp.tex: plot.exp.gpi out.exp.txt
	gnuplot $<

out.exp.txt : main.exe
	mono $< > $@

LIBDIR = ~/matlib
SRC := $(LIBDIR)/matrix/vector.cs
SRC += $(LIBDIR)/ode/ode.cs
matlib.dll : exp.cs $(SRC)
	mcs $^ -target:library -out:$@

clean :
	$(RM) *.dvi *.pdf *.aux
