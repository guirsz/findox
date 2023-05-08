CREATE OR REPLACE FUNCTION public.fn_users_create(
	in_user_name character varying,
	in_email character varying,
	in_password_hash bytea,
	in_password_salt bytea,
	in_role_id integer,
	in_deleted boolean,
	in_created_date timestamp without time zone,
	in_created_by integer)
    RETURNS integer
    LANGUAGE SQL
AS $BODY$

	INSERT INTO public.users(user_name, email, password_hash, password_salt, role_id, deleted, created_date, created_by, updated_date, updated_by)
	VALUES (in_user_name, in_email, in_password_hash, in_password_salt, in_role_id, in_deleted, in_created_date, in_created_by, in_created_date, in_created_by)
	RETURNING user_id;

$BODY$;

ALTER FUNCTION public.fn_users_create(character varying, character varying, bytea, bytea, integer, boolean, timestamp without time zone, integer)
    OWNER TO postgres;
