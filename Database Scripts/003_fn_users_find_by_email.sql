-- FUNCTION: public.fn_users_find_by_email(character varying)

-- DROP FUNCTION IF EXISTS public.fn_users_find_by_email(character varying);

CREATE OR REPLACE FUNCTION public.fn_users_find_by_email(
	user_email character varying)
    RETURNS users
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
SELECT 
	*
FROM users
WHERE email = user_email
$BODY$;

ALTER FUNCTION public.fn_users_find_by_email(character varying)
    OWNER TO postgres;
