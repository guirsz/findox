CREATE OR REPLACE FUNCTION public.fn_users_get_user_groups(
	in_user_id integer)
    RETURNS TABLE (group_id integer)
    LANGUAGE plpgsql
AS $BODY$
#variable_conflict use_column
BEGIN
	RETURN QUERY 
		SELECT 
			group_id
		FROM user_groups
		WHERE user_id = in_user_id;
END
$BODY$;

ALTER FUNCTION public.fn_users_get_user_groups(integer)
    OWNER TO postgres;