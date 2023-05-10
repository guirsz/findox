\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_users_get(
	in_user_id integer)
    RETURNS users
    LANGUAGE 'sql'
AS $BODY$

	SELECT 
		*
	FROM users
	WHERE user_id = in_user_id
	
$BODY$;

ALTER FUNCTION public.fn_users_get(integer)
    OWNER TO postgres;
