/*
Cursors 
Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.

Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.

Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.

Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.
*/

-- Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.

create table summary 
(customer_id int,
customer_name text,
rental_count int)

-- 1. Cursor insert data into summary table 
do $$
declare
	row_record record;
	row_record_cursor cursor for
		select r.customer_id,concat(c.first_name,' ', c.last_name) as Customer_Name, Count(r.customer_id) as Rental_Count
		from rental r
		join customer c on c.customer_id = r.customer_id
		group by r.customer_id,c.first_name, c.last_name
		order by Count(r.customer_id);
begin
	open row_record_cursor;
	loop
		fetch row_record_cursor into row_record;
		exit when not found;

		insert into summary(customer_id,customer_name,rental_count)
		values (
			row_record.customer_id,
			row_record.Customer_Name,
			row_record.Rental_Count
		);
	end loop;

	close row_record_cursor;
end;
$$;

select * from summary

-- 2. Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.

do $$
declare
	row_record record;
	row_record_cursor cursor for
		select f.title as filmTitle,Count(r.rental_id) rentalCount from film f
		join inventory i on i.film_id = f.film_id
		join rental r on r.inventory_id = i.inventory_id
		where f.film_id in (select film_id from film_category where category_id = 5)
		group by f.title
		having Count(r.rental_id)>10
		order by Count(r.rental_id) desc;
begin
	open row_record_cursor;
	loop
		fetch row_record_cursor into row_record;
		exit when not found;

		raise notice 'Film Name: %, Rental Count: %', row_record.filmTitle, row_record.rentalCount;
	end loop;

	close row_record_cursor;
end;
$$;

-- 3. Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.

select i.store_id, count(distinct  i.film_id) Flim_Count  from store s
join inventory i on i.store_id = s.store_id
group by i.store_id

create table report(
   store_id INT primary key,
   film_count INT
)

do $$
declare
	p_store_Id int;
	Flim_Count int;
	store_cursor cursor for
		select store_id from store;
begin
	open store_cursor;
	loop
		fetch store_cursor into p_store_Id;
		exit when not found;

		select count(distinct  i.film_id) 
		into Flim_Count
		from inventory i
		where i.store_id = p_store_Id;

		insert into report(store_id, film_count)
		values (p_store_Id,Flim_Count);
	end loop;

	close store_cursor;
end;
$$;

select*from report;

--4. Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.

create table inactive_customers(
inactive_id serial,
customer_id int
)

do $$
declare
    customer_rec record;
    cur cursor for
        select c.customer_id
        from customer c
        where not exists (
            select 1
            from rental r
            where r.customer_id = c.customer_id
              and r.rental_date > current_date - interval '6 months'
        );
begin
    open cur;

    loop
        fetch cur into customer_rec;
        exit when not found;

        insert into inactive_customers (customer_id)
		values (
            customer_rec.customer_id
        );
    end loop;

    close cur;
end $$;

select*from inactive_customers;

/*
Transactions 
Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.

Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.

Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.

Perform a transaction that transfers inventory from one store to another (delete + insert) safely.

Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity

*/

--1. Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.

begin;
do $$
declare
new_customer_id int;
new_rental_id int;
begin
-- 1. insert a new customer
insert into customer (store_id, first_name, last_name, email, address_id, activebool, create_date, active)
values (1, 'Srikanth', 'M', 'srikanth@gmail.com', 605, true, now(), 1)
returning customer_id into new_customer_id;
-- 2. Insert into rental table
insert into rental (rental_date, inventory_id, customer_id, staff_id)
values (now(), 1000, new_customer_id, 2)
returning rental_id into new_rental_id;
	
-- 3. insert payment for the rental
insert into payment (customer_id, staff_id, rental_id, amount, payment_date)
values (new_customer_id, 2, new_rental_id, 4.99, now());

end;
$$;

commit;

-- Verifying the Inserted values
select * from customer
where first_name = 'Srikanth'
select * from rental
where customer_id = 603
select * from payment
where customer_id = 603


-- 2. Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.
begin;
do $$
	begin
		update rental
		set staff_id = 20
		where rental_id = 166655;
		commit;
	exception
		when others then
			raise notice 'Invalid ID';
			rollback;
	end;
$$;
commit;

-- 3. Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.

begin;
update payment 
set amount = 5.99
where payment_id = 17503;

savepoint first_point;

update payment 
set amount = 5.999999999 --> Incorrect value as per the schema amount column only accepts numeric(5,2)
where payment_id = 17503; 

rollback to first_point;

commit;

-- Verifying the updated amount and it is now 5.99
select*from payment where payment_id = 17503

-- 4. Perform a transaction that transfers inventory from one store to another (delete + insert) safely.

begin;
do $$
declare
    p_film_id int;
begin
    select film_id into p_film_id
    from inventory
    where inventory_id = 1000 and store_id = 1;
    
    savepoint before_deletion;
	
    begin
        delete from inventory
        where inventory_id = 1000 and store_id = 1;
    exception
        when others then
            rollback to savepoint before_deletion;
            raise notice 'delete failed, rolled back to before_deletion';
    end;

    savepoint before_insert;
	
    begin
        insert into inventory (film_id, store_id)
        values (p_film_id, 2);
    exception
        when others then
            rollback to savepoint before_insert;
            raise notice 'insert failed, rolled back to before_insert';
    end;
end $$;
commit;

-- 5. Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity.

begin;

delete from payment
where customer_id = 1;

delete from rental
where customer_id = 1;

delete from customer
where customer_id = 1;

commit;

/*
Triggers
Create a trigger to prevent inserting payments of zero or negative amount.

Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.

Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week.
*/

-- 1. Create a trigger to prevent inserting payments of zero or negative amount.
create or replace function invalid_payment()
returns trigger as $$
begin
    if new.amount <= 0 then
        raise exception 'payment amount must be greater than zero.';
    end if;
    return new;
end;
$$ language plpgsql;

create trigger trigger_invalid_payment
before insert on payment
for each row
execute function invalid_payment();

-- 2. Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.
create or replace function update_last_update_on_film()
returns trigger 
as $$
begin
    if (new.title is distinct from old.title) or (new.rental_rate is distinct from old.rental_rate) then
        new.last_update := current_timestamp;
    end if;
    return new;
end;
$$ language plpgsql;

create trigger trigger_update_last_update
before update on film
for each row
execute function update_last_update_on_film();


-- 3.Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week.

create or replace function check_rental_count()
returns trigger
as $$
declare
    rental_count integer;
begin
    select count(r.inventory_id) into rental_count
    from rental r
    join inventory i on i.inventory_id = r.inventory_id
    join film f on f.film_id = i.film_id
    where f.film_id = new.film_id
      and r.rental_date >= current_date - interval '7 days'

    if rental_count > 10 then
        insert into rental_log (film_id, rental_count, log_date)
        values (new.film_id, rental_count, current_timestamp);
    end if;

    return new;
end;
$$ language plpgsql;

create trigger trigger_rental_count_check
after insert on rental
for each row
execute function check_rental_count();

