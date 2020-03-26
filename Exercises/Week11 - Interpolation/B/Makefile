
#all : out.txt
#	cat $<

SinPlot.svg : SinPlot.gpi out.txt
	gnuplot $<

out.txt: B.exe
	mono $< > $@

B.exe : B.cs spline.dll
	mcs $< -reference:$(word 2, $^)

spline.dll : ../A/spline.cs
	mcs $< -target:library -out:$@

.PHONEY:
clean:
	rm -f *.txt *.exe *.dll *.svg
