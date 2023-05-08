CREATE OR REPLACE FUNCTION public.fn_users_find_by_email(
	user_email character varying)
    RETURNS users
    LANGUAGE 'sql'
AS $BODY$
	SELECT 
		*
	FROM users
	WHERE email = user_email
$BODY$;

ALTER FUNCTION public.fn_users_find_by_email(character varying)
    OWNER TO postgres;
