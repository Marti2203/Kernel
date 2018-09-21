($define! combiner-core?
($lambda x
(apply and? (map ($lambda (x)
(or? (applicative? x)
(operative? x)))
x))))