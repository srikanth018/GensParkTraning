-- Phase 1 - Table Schema

/*
Tables to Design (Normalized to 3NF):
1. **students**
   * `student_id (PK)`, `name`, `email`, `phone`
2. **courses**
   * `course_id (PK)`, `course_name`, `category`, `duration_days`
3. **trainers**
   * `trainer_id (PK)`, `trainer_name`, `expertise`
4. **enrollments**
   * `enrollment_id (PK)`, `student_id (FK)`, `course_id (FK)`, `enroll_date`
5. **certificates**
   * `certificate_id (PK)`, `enrollment_id (FK)`, `issue_date`, `serial_no`
6. **course\_trainers** (Many-to-Many if needed)
   * `course_id`, `trainer_id`
*/

	
-- Phase 2 - DDL & DML
/*
* Create all tables with appropriate constraints (PK, FK, UNIQUE, NOT NULL)
* Insert sample data using `INSERT` statements
* Create indexes on `student_id`, `email`, and `course_id`
*/


create table students (
	student_id serial primary key,
	name varchar(50) not null, 
	email text unique not null,
	phone text not null 
		CHECK (char_length(phone) <= 10),
	last_updated timestamp default current_timestamp
);


create table courses (
	course_id serial primary key,
	course_name text not null, 
	category text not null,
	duration_days int not null,
	last_updated timestamp default current_timestamp
);

create table trainers (
	trainer_id serial primary key,
	trainer_name varchar(50) not null, 
	expertise varchar(100) not null,
	last_updated timestamp default current_timestamp
);


create table enrollments (
	enrollment_id serial primary key,
	student_id int not null, 
	course_id int not null,
	enroll_date date not null default current_date,
	last_updated timestamp default current_timestamp,

	foreign key (student_id) references students(student_id),
	foreign key (course_id) references courses(course_id)

);

create table certificates (
	certificate_id serial primary key,
	enrollment_id int not null, 
	issue_date date not null default current_date,
	serial_no text not null unique,
	last_updated timestamp default current_timestamp,
	foreign key (enrollment_id) references enrollments(enrollment_id)
);

alter table certificates
add constraint unique_serial_no unique (serial_no);


create table course_trainers (
	course_id int not null,
	trainer_id int not null,
	last_updated timestamp default current_timestamp,

	foreign key (course_id) references courses(course_id),
	foreign key (trainer_id) references trainers(trainer_id),
	primary key (course_id, trainer_id)
);


create index idx_students_student_id on students(student_id);
create index idx_students_email on students(email);
create index idx_courses_course_id on courses(course_id);


-- Inserting sample data to all the tables

INSERT INTO students (name, email, phone) VALUES
('Arun Kumar', 'arun.kumar@gmail.com', '9876543210'),
('Divya Ramesh', 'divya.ramesh@gmail.com', '9876512345'),
('Karthik Raju', 'karthik.raju@gmail.com', '9867543210'),
('Meena Sekar', 'meena.sekar@gmail.com', '9856741234'),
('Vignesh S', 'vignesh.s@gmail.com', '9845678901'),
('Lakshmi Priya', 'lakshmi.priya@gmail.com', '9823456789'),
('Srinivas P', 'srinivas.p@gmail.com', '9812345678'),
('Anjali Menon', 'anjali.menon@gmail.com', '9801234567'),
('Ravi Chandran', 'ravi.chandran@gmail.com', '9798765432'),
('Hari Dev', 'hari.dev@gmail.com', '9787654321');

INSERT INTO courses (course_name, category, duration_days) VALUES
('Python Programming', 'Software Development', 45),
('Data Science', 'Analytics', 60),
('Web Development', 'Software Development', 50),
('Cyber Security', 'Security', 40),
('Java Programming', 'Software Development', 55),
('Machine Learning', 'AI/ML', 65),
('Cloud Computing', 'IT Infrastructure', 35),
('UI/UX Design', 'Design', 30),
('DevOps Basics', 'Software Development', 25),
('SQL & Databases', 'Data Management', 40);

INSERT INTO trainers (trainer_name, expertise) VALUES
('Sundar Raj', 'Python, Data Science'),
('Anitha Lakshmi', 'Web Development, JavaScript'),
('Praveen Kumar', 'Cyber Security, Network Security'),
('Rajesh Iyer', 'Java, Spring Boot'),
('Deepa S', 'Machine Learning, AI'),
('Naveen Varma', 'AWS, Cloud Architecture'),
('Harini M', 'UI/UX, Figma'),
('Vasudevan K', 'DevOps, Docker, Jenkins'),
('Chitra Devi', 'SQL, PL/pgSQL'),
('Balaji R', 'Fullstack Development');

INSERT INTO enrollments (student_id, course_id) VALUES
(1,3),
(1,4),
(2,6),
(2,10),
(3,10),
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);


INSERT INTO certificates (enrollment_id, serial_no) VALUES
(11,'CERTU23453'),
(12,'CERTK89654'),
(15, 'CERTS14560'),
(13, 'CERTR16574'),
(1, 'CERTA12345'),
(3, 'CERTC54321'),
(5, 'CERTE11223'),
(6, 'CERTF33445'),
(8, 'CERTH99001'),
(10,'CERTJ55667');



INSERT INTO course_trainers (course_id, trainer_id) VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 3),
(5, 4),
(6, 5),
(7, 6),
(8, 7),
(9, 8),
(10, 9);

select * from students;
select * from courses;
select * from trainers;
select * from enrollments;
select * from certificates;
select * from course_trainers;

-- Phase 3: SQL Joins Practice

/*
Write queries to:

1. List students and the courses they enrolled in
2. Show students who received certificates with trainer names
3. Count number of students per course
*/

-- 1. List students and the courses they enrolled in

select s.name student_name, c.course_name
from enrollments e
join students s on s.student_id = e.student_id
join courses c on c.course_id = e.course_id
order by s.student_id;

-- 2. Show students who received certificates with trainer names

select s.name as student_name, c.serial_no, t.trainer_name
from enrollments e
join certificates c on c.enrollment_id = e.enrollment_id
join students s on s.student_id = e.student_id
join course_trainers ct on ct.course_id = e.course_id
join trainers t on t.trainer_id = ct.trainer_id

-- 3. Count number of students per course
select c.course_name, count(e.student_id) as No_of_Students	
from enrollments e
join courses c on c.course_id = e.course_id
group by c.course_name
order by count(e.student_id) desc

-- Phase 4: Functions & Stored Procedures
/*
Function:

Create `get_certified_students(course_id INT)`
→ Returns a list of students who completed the given course and received certificates.

Stored Procedure:

Create `sp_enroll_student(p_student_id, p_course_id)`
→ Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).
*/

-- 1. Create `get_certified_students(course_id INT)`
-- → Returns a list of students who completed the given course and received certificates.

create or replace function get_certified_students(p_course_id INT)
returns table (student_name varchar(20),serial_no text,trainer_name varchar(20), course_id int)
language plpgsql
as $$
begin

	return query
	
	select s.name as student_name, c.serial_no, t.trainer_name, e.course_id
	from enrollments e
	join certificates c on c.enrollment_id = e.enrollment_id
	join students s on s.student_id = e.student_id
	join course_trainers ct on ct.course_id = e.course_id
	join trainers t on t.trainer_id = ct.trainer_id
	where e.course_id = p_course_id;
end;
$$;

select * from get_certified_students(10);

-- 2.Create `sp_enroll_student(p_student_id, p_course_id)`
-- → Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).

create or replace procedure sp_enroll_student(p_student_id int, p_course_id int, p_status boolean)
language plpgsql
as $$
declare
	new_serial_no text;
	new_enroll_id int;
begin
	insert into enrollments(student_id,course_id) values (p_student_id,p_course_id)
	returning enrollment_id into new_enroll_id;

	if p_status then 
		new_serial_no := 'CERT'|| upper(substr(md5(random()::text),1,6));
		insert into certificates (enrollment_id, serial_no) values (new_enroll_id,new_serial_no);
	end if;
end;
$$;

call sp_enroll_student(1,8,true);
select * from enrollments order by enrollment_id desc;
select * from certificates order by enrollment_id desc;


-- Phase 5: Cursor

/*
Use a cursor to:

* Loop through all students in a course
* Print name and email of those who do not yet have certificates
*/

create or replace procedure get_students_without_certificate()
language plpgsql
as $$
declare 
	rec record;
	cur cursor for
		select s.name as student_name, s.email as Student_email
		from students s
		join enrollments e on e.student_id = s.student_id
		left outer join certificates c on c.enrollment_id = e.enrollment_id
		where c.certificate_id is null;
begin
	open cur;
	loop
		fetch cur into rec;
		exit when not found;

		raise notice 'Name: %, Email: %',rec.student_name, rec.Student_email;
	end loop;
end;
$$;

call get_students_without_certificate();

-- Phase 6: Security & Roles
/*

1. Create a `readonly_user` role:

   * Can run `SELECT` on `students`, `courses`, and `certificates`
   * Cannot `INSERT`, `UPDATE`, or `DELETE`

2. Create a `data_entry_user` role:

   * Can `INSERT` into `students`, `enrollments`
   * Cannot modify certificates directly
*/

-- 1. Create a `readonly_user` role:
create role readonly_user login password 'pass';

grant select on students, courses, certificates to readonly_user;

-- 2. Create a `data_entry_user` role:

create role data_entry_user login password 'pass';

grant insert on students, enrollments to data_entry_user;
grant select on students, enrollments to data_entry_user;

-- for checking about the roles and privilages
SELECT rolname FROM pg_roles;

SELECT grantee, privilege_type, table_name
FROM information_schema.role_table_grants
WHERE grantee IN ('readonly_user', 'data_entry_user');

-- Phase 7: Transactions & Atomicity

/*
Write a transaction block that:

* Enrolls a student
* Issues a certificate
* Fails if certificate generation fails (rollback)

```sql
BEGIN;
-- insert into enrollments
-- insert into certificates
-- COMMIT or ROLLBACK on error
```
*/

create or replace procedure sp_transaction_enroll_student(p_student_id int, p_course_id int, p_status boolean)
language plpgsql
as $$
declare
	new_serial_no text;
	new_enroll_id int;
begin
	insert into enrollments(student_id,course_id) values (p_student_id,p_course_id)
	returning enrollment_id into new_enroll_id;

	if p_status then 
		begin
			new_serial_no := 'CERT'|| upper(substr(md5(random()::text),1,6));
			insert into certificates (enrollment_id, serial_no) values (new_enroll_id,new_serial_no);
		exception
			when others then
				raise notice 'error in Certificate generation: %',sqlerrm; 
				rollback;
		end;
	end if;
exception
	when others then
		raise notice 'error in enrolling: %',sqlerrm; 
		rollback;
end;
$$;

begin;
	call sp_transaction_enroll_student(2,9,true);
commit;

select * from enrollments order by enrollment_id desc;
select * from certificates order by enrollment_id desc;
