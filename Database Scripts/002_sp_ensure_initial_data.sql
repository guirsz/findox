-- PROCEDURE: public.sp_ensure_initial_data(integer, character varying, character varying)

-- DROP PROCEDURE IF EXISTS public.sp_ensure_initial_data(integer, character varying, character varying);

CREATE OR REPLACE PROCEDURE public.sp_ensure_initial_data(
	IN user_role_id integer,
	IN admin_user_name character varying,
	IN admin_user_password character varying)
LANGUAGE 'sql'
AS $BODY$
	INSERT INTO users (user_name, password, role_id, created_date, created_by, updated_date, updated_by)
	SELECT admin_user_name, admin_user_password, user_role_id, CURRENT_TIMESTAMP, null, CURRENT_TIMESTAMP, null 
	WHERE NOT EXISTS (
		SELECT user_name FROM users WHERE user_name = admin_user_name
	);
$BODY$;
ALTER PROCEDURE public.sp_ensure_initial_data(integer, character varying, character varying)
    OWNER TO postgres;
