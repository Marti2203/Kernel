($define! length-core
($lambda (object)
($let (((#ignore #ignore a c)
($if (>? c 0)
#e+infinity
a))))