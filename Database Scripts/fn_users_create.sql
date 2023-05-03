-- FUNCTION: public.fn_users_create(integer, character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public.fn_users_create(integer, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public.fn_users_create(
	user_role_id integer,
	admin_user_name character varying,
	admin_user_email character varying,
	admin_user_password character varying)
    RETURNS integer
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
	INSERT INTO users (user_name, email, password, role_id, enabled, created_date, created_by, updated_date, updated_by)
	SELECT admin_user_name, admin_user_email, admin_user_password, user_role_id, 't', CURRENT_TIMESTAMP, null, CURRENT_TIMESTAMP, null 
	WHERE NOT EXISTS (
		SELECT user_name FROM users WHERE email = admin_user_email
	)
	RETURNING user_id;
$BODY$;

ALTER FUNCTION public.fn_users_create(integer, character varying, character varying, character varying)
    OWNER TO postgres;
