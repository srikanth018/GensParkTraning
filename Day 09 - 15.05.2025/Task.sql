create extension if not exists pgcrypto;

--  1. Create a stored procedure to encrypt a given text
-- Encryption Procedure 
drop procedure sp_encrypt_text;

create or replace procedure sp_encrypt_text(inputData TEXT, OUT encodedOutputData TEXT )
language plpgsql
as $$
declare 
 secKey TEXT := 'keyDay91505task';
 outputData BYTEA;
begin=
	select pgp_sym_encrypt(inputData, secKey) into outputData;
	select encode(outputData, 'base64') into encodedOutputData;
end;
$$;

do $$
declare 
encryptedData TEXT;
begin
	call sp_encrypt_text('sri@gmail.com', encryptedData);
	raise notice '%',encryptedData;
end;
$$;


-- Decrypt procedure
create or replace procedure sp_decrypt_text(inputData TEXT, OUT decodedOutputData TEXT )
language plpgsql
as $$
declare 
 secKey TEXT := 'keyDay91505task';
 decodeData BYTEA;
begin
	decodeData := decode(inputData, 'base64');
	select pgp_sym_decrypt(decodeData, secKey) into decodedOutputData;
end;
$$;

do $$
declare 
decodedData text;
begin
call sp_decrypt_text('ww0EBwMCo0Lr2lArsM9m0kMBwGlpNazzmadpVO2igeXC/65jzkhicd/N8p5RFHVKKmwlpd18xVWC
aK5rvsEi0nsFDS1Fm3MCy2nIXV9X2k6Fu5UI', decodedData);
raise notice '%',decodedData;
end;
$$;

-- 2. Create a stored procedure to compare two encrypted texts
-- Procedure for Compare two Encrypted data  
drop procedure sp_compare_encrypted

create or replace procedure sp_compare_encrypted(encryOne TEXT, encryTwo TEXT, out isSame int)
language plpgsql
as $$
declare 
	secKey TEXT := 'keyDay91505task';
	decryOne TEXT;
	decryTwo TEXT;
	decodeOne BYTEA;
	decodeTwo BYTEA;
begin

	decodeOne := decode(encryOne, 'base64');
	decodeTwo := decode(encryTwo, 'base64');
	
	select pgp_sym_decrypt(decodeOne,secKey) into decryOne;
	select pgp_sym_decrypt(decodeTwo,secKey) into decryTwo;

	if lower(decryOne) = lower(decryTwo) then
		isSame := 1;
	else
		isSame := 0;
	end if;
end;
$$;

do $$
declare 
encryptedData TEXT;
encryptedDatatwo TEXT;
isSame int;
begin
	call sp_encrypt_text('sri@gmail.com', encryptedData);
	call sp_encrypt_text('sri@gmail.com', encryptedDatatwo);
	call sp_compare_encrypted(encryptedData,encryptedDatatwo,isSame);
	raise notice '%',isSame;
	if isSame = 1 then 
		raise notice 'Equal!!!';
	else
		raise notice 'oops..!! Not Equal!!!';
	end if;
end;
$$;

--  3. Create a stored procedure to partially mask a given text
-- Procedure to mask data
create or replace procedure sp_mask_text(inputData TEXT, out maskedData TEXT)
language plpgsql
as $$
declare 
	len int := length(inputData);
begin
	if len <= 4 then
		maskedData := inputData;
	else 
		select substring(inputData from 1 for 2) || repeat('*',len-4) || substring(inputData from len-1 for 2) into maskedData;
	end if;
end;
$$;

do $$
declare 
maskedData text;
begin
call sp_mask_text('ArunKumar', maskedData);
raise notice '%',maskedData;
end;
$$;

--4. Create a procedure to insert into customer with encrypted email and masked name
-- Procedure to insert
create or replace procedure sp_insert_customer_data(p_store_id int, p_first_name varchar, p_last_name varchar, p_email text,
													 p_address_id int, p_activebool bool, p_active int)
language plpgsql
as $$
declare 
	v_masked_first_name TEXT;
	v_encrypted_email TEXT;
begin
	call sp_mask_text(p_first_name, v_masked_first_name);
	call sp_encrypt_text(p_email, v_encrypted_email);
	
	insert into customer(store_id, first_name, last_name, email, address_id, activebool,create_date,active)
	values (p_store_id, v_masked_first_name, p_last_name, v_encrypted_email, p_address_id, p_activebool,current_date, p_active);
exception 
	when others then
		raise notice 'Error Occured: %',sqlerrm;
end;
$$;

call sp_insert_customer_data(1,'Srikanth','M','srikanth@gmail.com',1,true,1);
call sp_insert_customer_data(1,'Mouly','Vinayaka','mvtn@gmail.com',1,true,1);
call sp_insert_customer_data(1,'Praveen','A','praveen@gmail.com',1,true,1);
call sp_insert_customer_data(1,'Arun','Kumar','ak@gmail.com',1,true,1);

-- 5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
-- Procedure for Displays customer_id, masked first name, and decrypted email 
create or replace procedure sp_read_customer_masked()
language plpgsql
as $$
declare 
	decrypted_mail text;
	customer_details record;
	cur cursor for
		select customer_id, first_name, email from customer where customer_id >= 605;
begin
	open cur;
	loop
		fetch cur into customer_details;
		exit when not found;
		
		call sp_decrypt_text(customer_details.email, decrypted_mail);
		
		raise notice '%, %, %',customer_details.customer_id,customer_details.first_name,decrypted_mail;
		
	end loop;
end;
$$;

call sp_read_customer_masked()
-------------------------------


select * from customer where customer_id >= 605;

alter table customer
alter column email type text;