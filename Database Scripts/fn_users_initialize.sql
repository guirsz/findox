-- FUNCTION: public.fn_users_initialize(integer, character varying, character varying, bytea, bytea)

-- DROP FUNCTION IF EXISTS public.fn_users_initialize(integer, character varying, character varying, bytea, bytea);

CREATE OR REPLACE FUNCTION public.fn_users_initialize(
	in_role_id integer,
	in_user_name character varying,
	in_email character varying,
	in_password_hash bytea,
	in_password_salt bytea)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	INSERT INTO users (user_name, email, password_hash, password_salt, role_id, deleted, created_date, created_by, updated_date, updated_by)
	SELECT in_user_name, in_email, in_password_hash, in_password_salt, in_role_id, FALSE, CURRENT_TIMESTAMP, null, CURRENT_TIMESTAMP, null 
	WHERE NOT EXISTS (
		SELECT user_name FROM users WHERE email = in_email
	)
	RETURNING user_id;
$BODY$;

ALTER FUNCTION public.fn_users_initialize(integer, character varying, character varying, bytea, bytea)
    OWNER TO postgres;
