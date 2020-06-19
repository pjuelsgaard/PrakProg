
SinInt.pdf : SinInt.tex SinIntPlot.tex
	pdflatex $<

SinIntPlot.tex: plot.gpi out.txt
	gnuplot $<

out.txt : si.exe
	mono $< > $@

si.exe : si.cs math.dll
	mcs $< -reference:$(word 2, $^) -out:$@


SRC = quado8a.cs
SRC += quadc7a.cs
SRC += quado4a.cs
math.dll : $(SRC)
	mcs $^ -target:library -out:$@


clean: 
	rm -f *.txt *.exe *.dll *.svg *.pdf
