
-- How can you retrieve all the information from the cd.facilities table?
select * from cd.facilities

-- You want to print out a list of all of the facilities and their cost to members. How would you retrieve a list of only facility names and costs?
select name, membercost from cd.facilities

-- How can you produce a list of facilities that charge a fee to members?
select * from cd.facilities where membercost != 0

-- How can you produce a list of facilities that charge a fee to members, and that fee is less than 1/50th of the monthly maintenance cost? Return the facid, facility name, member cost, and monthly maintenance of the facilities in question.
select facid, name, membercost, monthlymaintenance from cd.facilities 
where membercost != 0 and membercost < (monthlymaintenance/50)

-- How can you produce a list of all facilities with the word 'Tennis' in their name?
select * from cd.facilities 
where name Like '%Tennis%'

-- How can you retrieve the details of facilities with ID 1 and 5? Try to do it without using the OR operator.
select * from cd.facilities 
where facid in (1,5)

-- How can you produce a list of facilities, with each labelled as 'cheap' or 'expensive' depending on if their monthly maintenance cost is more than $100? Return the name and monthly maintenance of the facilities in question.
select name, 
case when monthlymaintenance > 100 then 'expensive'
else 'cheap'
end as cost
from cd.facilities

-- How can you produce a list of members who joined after the start of September 2012? Return the memid, surname, firstname, and joindate of the members in question.
select memid, surname, firstname, joindate from cd.members
where extract (month from joindate) >= 9 and extract(year from joindate) >= 2012 

-- How can you produce an ordered list of the first 10 surnames in the members table? The list must not contain duplicates.
select distinct surname from cd.members
order by surname
limit 10;

-- You, for some reason, want a combined list of all surnames and all facility names. Yes, this is a contrived example :-). Produce that list!
select name from cd.facilities
union
select surname from cd.members

-- You'd like to get the signup date of your last member. How can you retrieve this information?
select max(joindate) latest from cd.members

-- You'd like to get the first and last name of the last member(s) who signed up - not just the date. How can you do that?
select firstname, surname, joindate latest from cd.members
where joindate = (select max(joindate) from cd.members) ;

-- Topic : Joins and subqueries

-- How can you produce a list of the start times for bookings by members named 'David Farrell'?
select b.starttime 
from cd.bookings b
join cd.members m on m.memid = b.memid
where m.firstname = 'David' and m.surname = 'Farrell'

-- How can you produce a list of the start times for bookings for tennis courts, for the date '2012-09-21'? Return a list of start time and facility name pairings, ordered by the time.
select b.starttime, f.name from cd.facilities f
join cd.bookings b on b.facid = f.facid
where f.name like 'Tennis%' and date(b.starttime) = '2012-09-21'
order by b.starttime

-- How can you output a list of all members who have recommended another member? Ensure that there are no duplicates in the list, and that results are ordered by (surname, firstname).
select distinct m.firstname, m.surname from cd.members m
join cd.members r on r.recommendedby = m.memid
order by surname, firstname
