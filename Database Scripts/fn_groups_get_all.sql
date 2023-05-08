CREATE OR REPLACE FUNCTION public.fn_groups_get_all()
	RETURNS TABLE (
		group_id integer,
		group_name character varying
	) 
    LANGUAGE plpgsql
AS $BODY$
#variable_conflict use_column
BEGIN	
	RETURN QUERY 
		SELECT 
			group_id,
			group_name
		FROM groups
		WHERE deleted = FALSE
		ORDER BY group_name;
END
$BODY$;

ALTER FUNCTION public.fn_groups_get_all()
    OWNER TO postgres;
