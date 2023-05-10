\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_users_get_all_paginated(
	in_limit integer,
	in_offset integer,
	in_filter_text character varying)
	RETURNS TABLE (
		user_id integer,
		user_name character varying,
		email character varying,
		role_id integer,
		groups integer[]
	) 
    LANGUAGE plpgsql
AS $BODY$
#variable_conflict use_column
BEGIN	
	RETURN QUERY 
		SELECT 
			user_id,
			user_name,
			email,
			role_id,
			array(select user_groups.group_id from user_groups where users.user_id = user_groups.user_id) as groups
		FROM users
		WHERE 
			user_name ILIKE in_filter_text 
			AND deleted = FALSE
		ORDER BY user_name
		LIMIT in_limit OFFSET in_offset;
END
$BODY$;

ALTER FUNCTION public.fn_users_get_all_paginated(integer, integer, character varying)
    OWNER TO postgres;
