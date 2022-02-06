using kolo2.Data;
using kolo2.DTOs;
using kolo2.Entities;
using kolo2.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace kolo2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodesController : ControllerBase
    {
        private readonly CustomDBContext _dbContext;

        public NodesController(CustomDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Node FindNodeById(int id)
        {
            try
            {
                var nodes = _dbContext.nodes.Where(node => node.id == id);
                return nodes.Any() ? nodes.First() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Employee FindEmployeeById(int id)
        {
            try
            {
                var emps = _dbContext.employees.Where(emp => emp.id == id);
                return emps.Any() ? emps.First() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private NodeDTO Item2dto(Node node)
        {
            NodeDTO dto = DTOFormatter.Convert(node);
            if (node.node_boss is int @id)
            {
                Employee boss = FindEmployeeById(@id);
                dto.Boss = DTOFormatter.Convert(boss).Fullname;
            }
            if (node.node_super is int @id2)
            {
                Node super = FindNodeById(@id2);
                dto.Super = super is null ? "" : super.node_name;
            }
            return dto;
        }

        // GET /Nodes
        [HttpGet]
        public ActionResult GetNodes()
        {
            try
            {
                return Ok(_dbContext.nodes.ToList().Select(Item2dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetNode(int id)
        {
            Node node = FindNodeById(id);
            if (node is null) return NotFound();
            NodeDTO nodeDTO = Item2dto(node);
            if (nodeDTO is null) return NotFound();
            return Ok(nodeDTO);
        }

        private string ValidateInput(SetNodeDTO node)
        {
            if (node is null) return "Missing input!";
            // ak je uvedeny nadriadeny uzol, tak skontrolovat jeho typ
            if (node.node_super is int @sup)
            {
                Node super = FindNodeById(@sup);
                if (super is null) return "Super node does not exist!";
                if (super.node_type != node.node_type - 1)
                {
                    return "Super Node of '" + node.node_name + "' must be a '"
                        + ((NodeType) super.node_type).ToString() + "'!";
                }
            }
            // veduci / riaditel musí existovať v tabuľke zamestnancov
            if (node.node_boss is int @bss)
            {
                Employee boss = FindEmployeeById(@bss);
                if (boss is null) return "Employee with such id does not exist!";
            }
            return null;
        }

        [HttpPost("add-node")]
        public ActionResult<EmployeeDTO> AddNode([FromBody] SetNodeDTO node)
        {
            var err = ValidateInput(node);
            if (err is not null) return BadRequest(err);

            Node newNode = new()
            {
                node_name = node.node_name,
                node_type = node.node_type,
                node_boss = node.node_boss,
                node_super = node.node_super
            };

            try
            {
                _dbContext.nodes.Add(newNode);
                _dbContext.SaveChanges();
                return Ok(Item2dto(newNode));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("modify-node/{id}")]
        public ActionResult UpdateNode(int id, [FromBody] SetNodeDTO node)
        {
            var err = ValidateInput(node);
            if (err is not null) return BadRequest(err);

            Node mnode = FindNodeById(id);
            if (mnode is null) return NotFound();
            mnode.node_name = node.node_name;
            mnode.node_type = node.node_type;
            mnode.node_boss = node.node_boss;
            mnode.node_super = node.node_super;
            try
            {
                _dbContext.nodes.Update(mnode);
                _dbContext.SaveChanges();
                return Ok(Item2dto(mnode));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("remove-node/{id}")]
        public ActionResult DeleteNode(int id)
        {
            Node node = FindNodeById(id);
            if (node is null) return NotFound();
            try
            {
                _dbContext.nodes.Remove(node);
                _dbContext.SaveChanges();
                return Ok(DTOFormatter.Convert(node));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
