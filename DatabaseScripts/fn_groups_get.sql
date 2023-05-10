\c findoxdb;

CREATE OR REPLACE FUNCTION public.fn_groups_get(
	in_group_id integer)
    RETURNS groups
    LANGUAGE 'sql'
AS $BODY$

	SELECT 
		*
	FROM groups
	WHERE group_id = in_group_id
	
$BODY$;

ALTER FUNCTION public.fn_groups_get(integer)
    OWNER TO postgres;
