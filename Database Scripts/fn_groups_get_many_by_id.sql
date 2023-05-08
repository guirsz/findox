CREATE OR REPLACE FUNCTION public.fn_groups_get_many_by_id(
	in_group_id integer[])
    RETURNS TABLE (
		group_id    	integer,
		group_name  	character varying,
		deleted			boolean,
		created_date	timestamp without time zone,
		created_by 		integer,
		updated_date	timestamp without time zone,
		updated_by		integer
	)
    LANGUAGE plpgsql
AS $BODY$
#variable_conflict use_column
BEGIN
	RETURN QUERY 
		SELECT 
			group_id,
			group_name,
			deleted,
			created_date,
			created_by, 	
			updated_date,
			updated_by
		FROM user_groups
		WHERE group_id = ANY(in_group_id);
END
$BODY$;

ALTER FUNCTION public.fn_groups_get_many_by_id(integer[])
    OWNER TO postgres;