# start thebox 
5 February 2021, 10:10 AM

echo "# thebox" >> README.md

git init

git add README.md

git commit -m "first commit"

git branch -M main

git remote add origin https://github.com/ppongsakorn/thebox.git

git push -u origin main

---

# update the box
git status

git add *.* //sth name

git commit -m "sth about ..."

git push //curent local branch to remote branch
