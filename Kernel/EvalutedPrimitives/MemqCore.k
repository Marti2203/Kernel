($define! memq-core?
($lambda (object ls)
(apply or?
(map ($lambda (x) (eq? object x))
ls))))