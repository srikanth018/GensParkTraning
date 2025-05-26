-- List all films with their length and rental rate, sorted by length descending. Columns: title, length, rental_rate

select title, length,rental_rate  from film
order by length

-- Find the top 5 customers who have rented the most films. Hint: Use the rental and customer tables.

select concat(c.first_name,' ',c.last_name) as CustomerName , Count(r.customer_id) as rentalCount from rental r
join customer c on c.customer_id = r.customer_id  
group by r.customer_id,c.first_name,c.last_name
order by Count(r.customer_id) desc
limit 5

-- Display all films that have never been rented. Hint: Use LEFT JOIN between film and inventory → rental.

select distinct f.film_id, f.title
from film f
left join inventory i on i.film_id = f.film_id
left join rental r on r.inventory_id = i.inventory_id 
order by f.film_id

-- List all actors who appeared in the film ‘Academy Dinosaur’. Tables: film, film_actor, actor

select Concat(a.first_name,' ',a.last_name) as ActorName
from film f
join film_actor fa on fa.film_id = f.film_id
join actor a on a.actor_id = fa.actor_id
where f.title = 'Academy Dinosaur'

-- List each customer along with the total number of rentals they made and the total amount paid. Tables: customer, rental, payment

select r.customer_id, concat(c.first_name,' ',c.last_name) as CustomerName , sum(p.amount) as TotalAmountRented
from rental r 
join payment p on p.rental_id = r.rental_id
join customer c on c.customer_id = r.customer_id
group by r.customer_id,c.first_name,c.last_name
order by r.customer_id

-- Using a CTE, show the top 3 rented movies by number of rentals. Columns: title, rental_count

with RentedCount
as
(
	select f.title as FilmName, count(r.inventory_id) as RentedCounts
	from film f
	join inventory i on i.film_id = f.film_id
	join rental r on r.inventory_id = i.inventory_id 
	group by r.inventory_id, f.title
)
select * from RentedCount
order by RentedCounts desc
limit 3

-- Find customers who have rented more than the average number of films. Use a CTE to compute the average rentals per customer, then filter.

with rental_counts as
(
  select cu.customer_id, concat(cu.first_name,' ',cu.last_name) as customer_name, count(re.rental_id) as total_rentals from customer cu
  join rental re on re.customer_id = cu.customer_id
  group by cu.customer_id
),
average_rentals as (
select avg(total_rentals * 1.0) as avg_rentals from rental_counts
)
select rc.customer_id, rc.customer_name,rc.total_rentals,a.avg_rentals from rental_counts rc
join average_rentals a on rc.total_rentals>a.avg_rentals
order by rc.total_rentals desc;

-- Write a function that returns the total number of rentals for a given customer ID. Function: get_total_rentals(customer_id INT)

Create function get_total_rentals(p_customer_id INT)
returns int
language plpgsql
as
$$
declare totalRental int;
begin
	select Count(*)
	into totalRental
	from rental
	where customer_id = p_customer_id;
	return totalRental;
end;
$$;

select get_total_rentals(2)



-- Write a stored procedure that updates the rental rate of a film by film ID and new rate.
-- Procedure: update_rental_rate(film_id INT, new_rate NUMERIC)

Create procedure update_rental_rate(p_film_id INT, p_new_rate NUMERIC)
language plpgsql
as $$
begin
	update film set rental_rate = p_new_rate where film_id = p_film_id;
end;
$$;

select * from film
order by film_id

Call update_rental_rate (1, 4.99)

-- Write a procedure to list overdue rentals (return date is NULL and rental date older than 7 days).
-- Procedure: get_overdue_rentals() that selects relevant columns.

Create or replace procedure get_overdue_rentals ()
returns table (rental_id,rental_date,inventory_id,customer_id,return_date,staff_id,last_update)
language plpgsql
as $$
begin
	select * from rental
	where return_date is null or (date(return_date)-date(rental_date)) >7;
end;
$$;


SELECT * FROM get_overdue_rentals();

