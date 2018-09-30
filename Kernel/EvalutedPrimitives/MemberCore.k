($define! member-core?
($lambda (object ls)
(apply or?
(map ($lambda (x) (equal? object x))
ls))))