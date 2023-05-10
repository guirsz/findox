\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_users_unlink_group(
	in_user_id integer,
	in_group_id integer[])
    RETURNS void
    LANGUAGE SQL
AS $BODY$

	DELETE FROM 
		public.user_groups 
	WHERE
		user_id = in_user_id 
		AND group_id = ANY(in_group_id)

$BODY$;

ALTER FUNCTION public.fn_users_unlink_group(integer, integer[])
    OWNER TO postgres;