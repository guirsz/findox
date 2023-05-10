\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_groups_get_by_name(
	in_group_name character varying)
    RETURNS groups
    LANGUAGE 'sql'
AS $BODY$

	SELECT 
		*
	FROM groups
	WHERE group_name = in_group_name
	
$BODY$;

ALTER FUNCTION public.fn_groups_get_by_name(character varying)
    OWNER TO postgres;
