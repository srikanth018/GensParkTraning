-- Triggers in PostgreSQL
-- drop trigger trg_log_customer_email_Change on customer --> to drop a trigger

create table audit_log
(audit_id serial primary key,
table_name text,
field_name text,
old_value text,
new_value text,
updated_date Timestamp default current_Timestamp);


-- Trigger example without parameter

create or replace function Update_Audit_log()
returns trigger 
as $$
begin
	Insert into audit_log(table_name,field_name,old_value,new_value,updated_date) 
	values('customer','email',OLD.email,NEW.email,current_Timestamp);
	return new;
end;
$$ language plpgsql


create trigger trg_log_customer_email_Change
before update
on customer
for each row
execute function Update_Audit_log();

select * from customer order by customer_id

select * from audit_log
update customer set email = 'mary.smith@sakilacustomer.org' where customer_id = 1


-- Trigger with parameter

create or replace function Update_Audit_log_Param()
returns trigger 
as $$
declare 
   col_name text := TG_ARGV[0];
   tab_name text := TG_ARGV[1];
   o_value text;
   n_value text;
begin
    EXECUTE FORMAT('select ($1).%I::TEXT', COL_NAME) INTO O_VALUE USING OLD;
	EXECUTE FORMAT('select ($1).%I::TEXT', COL_NAME) INTO N_VALUE USING NEW;
	if o_value is distinct from n_value then
		Insert into audit_log(table_name,field_name,old_value,new_value,updated_date) 
		values(tab_name,col_name,o_value,n_value,current_Timestamp);
	end if;
	return new;
end;
$$ language plpgsql


create trigger trg_log_customer_email_Change_Param
after update
on customer
for each row
execute function Update_Audit_log('last_name','customer');



select * from audit_log
update customer set last_name = 'Smith' where customer_id = 1;
update customer set last_name = 'Johnson' where customer_id = 2;

-- Cursor:
-- Example

do $$
declare
    rental_record record;
    rental_cursor cursor for
        select r.rental_id, c.first_name, c.last_name, r.rental_date
        from rental r
        join customer c on r.customer_id = c.customer_id
        order by r.rental_id;
begin
    open rental_cursor;

    loop
        fetch rental_cursor into rental_record;
        exit when not found;

        raise notice 'rental id: %, customer: % %, date: %',
                     rental_record.rental_id,
                     rental_record.first_name,
                     rental_record.last_name,
                     rental_record.rental_date;
    end loop;

    close rental_cursor;
end;
$$;

-- Insert data in the table using cursor

create table rental_tax_log (
    rental_id int,
    customer_name text,
    rental_date timestamp,
    amount numeric,
    tax numeric
);

select * from rental_tax_log
truncate table rental_tax_log


do $$
declare
    rec record;
    cur cursor for
        select r.rental_id, 
               c.first_name || ' ' || c.last_name as customer_name,
               r.rental_date,
               p.amount
        from rental r
        join payment p on r.rental_id = p.rental_id
        join customer c on r.customer_id = c.customer_id;
begin
    open cur;

    loop
        fetch cur into rec;
        exit when not found;

        insert into rental_tax_log (rental_id, customer_name, rental_date, amount, tax)
        values (
            rec.rental_id,
            rec.customer_name,
            rec.rental_date,
            rec.amount,
            rec.amount * 0.10
        );
    end loop;

    close cur;
end;
$$;



-- Cursor inside the Storded Procedure

create or replace procedure Calculate_Tax_For_Each(p_tax numeric(10,2), p_amount numeric)
language plpgsql
as $$
declare 
	row_data record;
	row_data_cursor cursor for
		select r.rental_id, 
               c.first_name || ' ' || c.last_name as customer_name,
               r.rental_date,
               p.amount
        from rental r
        join payment p on r.rental_id = p.rental_id
        join customer c on r.customer_id = c.customer_id
		where amount > p_amount;
		
begin
	open row_data_cursor;
	loop
		fetch row_data_cursor into row_data;
		exit when not found;

		insert into rental_tax_log (rental_id, customer_name, rental_date, amount, tax)
        values (
            row_data.rental_id,
            row_data.customer_name,
            row_data.rental_date,
            row_data.amount,
            row_data.amount * p_tax
        );
	end loop;

	close row_data_cursor;
end;
$$;

call Calculate_Tax_For_Each(0.10, 5);