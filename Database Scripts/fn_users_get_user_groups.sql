CREATE OR REPLACE FUNCTION public.fn_users_get_user_groups(
	in_user_id integer)
    RETURNS TABLE (group_id integer)
    LANGUAGE plpgsql
AS $BODY$
#variable_conflict use_column
BEGIN
	RETURN QUERY 
		SELECT 
			user_groups.group_id
		FROM user_groups
		INNER JOIN groups ON user_groups.group_id = groups.group_id AND groups.deleted = FALSE
		WHERE user_groups.user_id = in_user_id;
END
$BODY$;

ALTER FUNCTION public.fn_users_get_user_groups(integer)
    OWNER TO postgres;