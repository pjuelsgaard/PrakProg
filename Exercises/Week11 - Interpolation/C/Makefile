
#all : out.txt
#	cat $<

SinPlot.svg PlotCompare.svg: SinPlot.gpi SplineCompare.gpi out.txt
	gnuplot $<
	gnuplot $(word 2, $^)

out.txt: C.exe
	mono $< > $@

C.exe : C.cs spline.dll
	mcs $< -reference:$(word 2, $^)

spline.dll : ../A/spline.cs
	mcs $< -target:library -out:$@

data.txt : sin.exe
	mono $< > $@
sin.exe : sin.cs
	mcs $<

.PHONEY:
clean:
	rm -f *.txt *.exe *.dll *.svg
